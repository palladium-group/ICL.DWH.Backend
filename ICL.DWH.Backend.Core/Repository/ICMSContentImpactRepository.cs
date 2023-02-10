﻿using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Repository
{
    public interface ICMSContentImpactRepository : IRepository<CMSContentImpact>
    {
        public IQueryable<CMSContentImpact> GetContentImpactByRol(string roleId);
        public IQueryable<CMSContentImpact> GetContentImpactByName(string name);
    }
}
