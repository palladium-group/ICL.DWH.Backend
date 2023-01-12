using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Utils
{
    public class Transaction
    {
        public string? ID { get; set; }
        public string? Status { get; set; }
        public string? CutomerRefNo { get; set; }
        public string? TransactionId { get; set; }
    }

    public class ArrayOfTransaction
    {
        public Transaction? Transaction { get; set; }
        public string? I { get; set; }
        public string? Text { get; set; }
    }

    public class SCMResponse
    {
        public string? access_token { get; set; }
        public string? token_type { get; set; }
    }

    public class ContactInformation
    {
        public string ContactName { get; set; }
        public int ContactNumber { get; set; }
        public string ContactEmail { get; set; }
    }

    public class PartnerInformation
    {
        public string PartnerIdentifier { get; set; }
        public string PartnerName { get; set; }
        public ContactInformation ContactInformation { get; set; }
    }

    public class Partners
    {
        public PartnerInformation PartnerInformation { get; set; }
    }

    public class Header
    {
        public string MessageType { get; set; }
        public int MessageVersion { get; set; }
        public string MessageIdentifier { get; set; }
        public double MessageDateTime { get; set; }
        public Partners Partners { get; set; }
    }

    public class ServiceClass
    {
        public string Code { get; set; }
    }

    public class Client
    {
        public string Code { get; set; }
    }

    public class Shipper
    {
        public string Code { get; set; }
    }

    public class Consignee
    {
        public string Code { get; set; }
    }

    public class AccountManagement
    {
        public object SalesPIC { get; set; }
        public object CustomerServicePIC { get; set; }
    }

    public class PlaceOfReceipt
    {
        public string Code { get; set; }
    }

    public class PlaceOfDelivery
    {
        public string Code { get; set; }
    }

    public class RequestedShipmentDate
    {
        public int Date { get; set; }
        public int Time { get; set; }
        public string TimeZone { get; set; }
    }

    public class RequestedArrivalDate
    {
        public int Date { get; set; }
        public int Time { get; set; }
        public string TimeZone { get; set; }
    }

    public class Movement
    {
        public PlaceOfReceipt PlaceOfReceipt { get; set; }
        public PlaceOfDelivery PlaceOfDelivery { get; set; }
        public RequestedShipmentDate RequestedShipmentDate { get; set; }
        public RequestedArrivalDate RequestedArrivalDate { get; set; }
        public string TransportationMode { get; set; }
    }

    public class PackageSummary
    {
        public object TotalPackages { get; set; }
        public object Weight { get; set; }
    }

    public class BookingOffice
    {
        public string Code { get; set; }
    }

    public class SystemInformation
    {
        public BookingOffice BookingOffice { get; set; }
        public string Source { get; set; }
    }

    public class BasicDetails
    {
        public string BookingType { get; set; }
        public string BookingNo { get; set; }
        public int BookingDate { get; set; }
        public ServiceClass ServiceClass { get; set; }
        public Client Client { get; set; }
        public Shipper Shipper { get; set; }
        public Consignee Consignee { get; set; }
        public string ConsigneeReferenceNo { get; set; }
        public AccountManagement AccountManagement { get; set; }
        public Movement Movement { get; set; }
        public PackageSummary PackageSummary { get; set; }
        public SystemInformation SystemInformation { get; set; }
    }

    public class PartyName
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public string Code { get; set; }
    }

    public class AddressInformation
    {
        public string City { get; set; }
        public Country Country { get; set; }
    }

    public class Party
    {
        public PartyName PartyName { get; set; }
        public string PartyType { get; set; }
        public AddressInformation AddressInformation { get; set; }
    }

    public class Parties
    {
        public List<Party> Party { get; set; }
    }

    public class Quantity
    {
        public int Value { get; set; }
        public string Uom { get; set; }
    }

    public class UnitDimension
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Uom { get; set; }
    }

    public class UnitWeight
    {
        public int NetWeight { get; set; }
        public int GrossWeight { get; set; }
        public string Uom { get; set; }
    }

    public class OrderDetails
    {
        public object CountryOfManufacturing { get; set; }
        public string SKULineNo { get; set; }
    }

    public class Products
    {
        public Product Product { get; set; }
    }

    public class PortOfLoading
    {
        public string Code { get; set; }
    }

    public class PortOfDischarge
    {
        public string Code { get; set; }
    }

    public class ETD
    {
        public int Date { get; set; }
        public int Time { get; set; }
        public string TimeZone { get; set; }
    }

    public class ETA
    {
        public int Date { get; set; }
        public int Time { get; set; }
        public string TimeZone { get; set; }
    }

    public class OwnerOfficeorSite
    {
        public string Code { get; set; }
    }

    public class ExecutingOfficeorSite
    {
        public string Code { get; set; }
    }

    public class Sea
    {
        public object VoyageDate { get; set; }
    }

    public class Air
    {
        public object FlightDate { get; set; }
    }

    public class Road
    {
        public object PickupWindowFromDate { get; set; }
        public object PickupWindowToDate { get; set; }
        public object DeliveryWindowFromDate { get; set; }
        public object DeliveryWindowToDate { get; set; }
    }

    public class Service
    {
        public string ServiceType { get; set; }
        public string ProcessType { get; set; }
        public int ServiceSequenceNo { get; set; }
        public PlaceOfReceipt PlaceOfReceipt { get; set; }
        public PlaceOfDelivery PlaceOfDelivery { get; set; }
        public PortOfLoading PortOfLoading { get; set; }
        public PortOfDischarge PortOfDischarge { get; set; }
        public ETD ETD { get; set; }
        public ETA ETA { get; set; }
        public object ServiceClass { get; set; }
        public string ManagedBy { get; set; }
        public OwnerOfficeorSite OwnerOfficeorSite { get; set; }
        public ExecutingOfficeorSite ExecutingOfficeorSite { get; set; }
        public int PrimaryService { get; set; }
        public Sea Sea { get; set; }
        public Air Air { get; set; }
        public Road Road { get; set; }
        public object Warehouse { get; set; }
        public object Carrier { get; set; }
        public string Action { get; set; }
    }

    public class Services
    {
        public Service Service { get; set; }
    }

    public class Booking
    {
        public string Action { get; set; }
        public BasicDetails BasicDetails { get; set; }
        public Parties Parties { get; set; }
        public Products Products { get; set; }
        public Services Services { get; set; }
    }

    public class Bookings
    {
        public Booking Booking { get; set; }
    }

    public class Message
    {
        public Header Header { get; set; }
        public Bookings Bookings { get; set; }
        public string Xsi { get; set; }
        public string Text { get; set; }
    }
}
