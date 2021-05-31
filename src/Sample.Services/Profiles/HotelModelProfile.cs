using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Sample.Services.Models;

namespace Sample.Services.Profiles
{
    public class HotelModelProfile:Profile
    {
        public HotelModelProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<HotelModel, HotelDocumentManagementModel>();
        }
    }
}
