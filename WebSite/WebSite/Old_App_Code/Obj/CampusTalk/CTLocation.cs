﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.App_Code.Obj.CampusTalk
{
    public class CTLocation
    {
        string longitude;
        string latitude;
        string uid;
        string datetime;

        public string Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
            }
        }

        public string Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }

        public string Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        public string Datetime
        {
            get
            {
                return datetime;
            }

            set
            {
                datetime = value;
            }
        }
    }
}