using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ICL.DWH.Backend.Core.Utils;

namespace ICL.DWH.Backend.Core.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private static readonly HttpClient _httpClient = new HttpClient();

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderRepository.GetAll();
        }

        public PurchaseOrder NewPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                var result = _purchaseOrderRepository.GetAll(x => x.BookingNo == purchaseOrder.BookingNo).FirstOrDefault();
                if (result == null)
                {
                    return _purchaseOrderRepository.Create(purchaseOrder);
                }
                result.AsnFile = purchaseOrder.AsnFile;
                return _purchaseOrderRepository.Update(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                return _purchaseOrderRepository.Update(purchaseOrder);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseOrder FindByBoookingNo(string BookingNo)
        {
            try
            {
                var result = _purchaseOrderRepository.GetAll(x => x.BookingNo == BookingNo).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePurchaseOrderAsFailed(string bookingId, string errorMessage)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId && x.Status == PurchaseOrderStatus.Pending).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    purchaseOrder.Status = PurchaseOrderStatus.Failed;
                    purchaseOrder.ErrorMessage = errorMessage;
                    _purchaseOrderRepository.Update(purchaseOrder);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdatePurchaseOrderByScmId(string bookingId, Guid scmId)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    purchaseOrder.SCMID = scmId;
                    purchaseOrder.Status = PurchaseOrderStatus.Delivered;
                    _purchaseOrderRepository.Update(purchaseOrder);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ValidatePurchaseOrder(string bookingId)
        {
            try
            {
                var purchaseOrder = _purchaseOrderRepository.GetAll(x => x.BookingNo == bookingId && x.Status == PurchaseOrderStatus.Pending).FirstOrDefault();
                if (purchaseOrder != null)
                {
                    if (purchaseOrder.BookingDate != null && purchaseOrder.BookingNo != null)
                    {
                        return "valid";
                    }
                    else
                    {
                        return "not valid";
                    }
                }
                else
                {
                    return "not valid";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> PostToSCMAsync(string mySbMsg, string bookingNo)
        {
            try
            {
                //push message to scm-profit
                var requestContent = new StringContent("grant_type=password&username=fitexpress&password=FitExpress@2021");
                var response = await _httpClient.PostAsync("http://fitnewuat.hht.freightintime.com/token", requestContent);
                var responseBody = await response.Content.ReadAsAsync<SCMResponse>();
                var token = responseBody.access_token;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var bookingRequestContent = new StringContent(mySbMsg);
                var bookingResponse = await _httpClient.PostAsync("http://fitnewuat.hht.freightintime.com/api/v1/Booking/ProcessBooking", bookingRequestContent);
                var responseContent = await bookingResponse.Content.ReadAsStringAsync();

                if (bookingResponse.IsSuccessStatusCode)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTransaction));
                    using (StringReader reader = new StringReader(responseContent))
                    {
                        var scmResponse = (ArrayOfTransaction)serializer.Deserialize(reader);
                        PurchaseOrder po = (PurchaseOrder)FindByBoookingNo(bookingNo);

                        if (scmResponse.Transaction.Status == "Fail")
                        {
                            var errorString = JsonConvert.SerializeObject(responseContent);

                            po.ErrorMessage = errorString;
                            po.Status = PurchaseOrderStatus.Failed;
                            UpdatePurchaseOrder(po);
                        }
                        else
                        {
                            var transactionId = scmResponse.Transaction.TransactionId;

                            po.ErrorMessage = "";
                            po.Status = PurchaseOrderStatus.Delivered;
                            po.SCMID = new Guid(transactionId);
                            UpdatePurchaseOrder(po);
                        }
                    }
                }

                return "Submitted";
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
