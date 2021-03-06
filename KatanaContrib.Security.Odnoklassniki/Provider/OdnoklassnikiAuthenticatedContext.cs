﻿using System;
using System.Globalization;
using System.Security.Claims;
using System.Xml;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace KatanaContrib.Security.Odnoklassniki
{
    public class OdnoklassnikiAuthenticatedContext : BaseContext
    {
        public OdnoklassnikiAuthenticatedContext(IOwinContext context, JObject user, string accessToken, string expires)
            : base(context)
        {
            User = user;
            AccessToken = accessToken;

            int expiresValue;
            if (Int32.TryParse(expires, NumberStyles.Integer, CultureInfo.InvariantCulture, out expiresValue))
            {
                ExpiresIn = TimeSpan.FromSeconds(expiresValue);
            }

            Id = TryGetValue("uid");
            Name = TryGetValue("first_name");
            LastName = TryGetValue("last_name");
            UserName = TryGetValue("name");
            Link = TryGetValue("pic_1");
        }

        public JObject User { get; private set; }
        public string AccessToken { get; private set; }
        public TimeSpan? ExpiresIn { get; set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }
        public string DefaultName
        {
            get
            {
                if (!String.IsNullOrEmpty(UserName))
                    return UserName;

                return FullName;
            }
        }
        public string Link { get; private set; }
        public string UserName { get; private set; }
        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }

        private string TryGetValue(string propertyName)
        {
            return User[propertyName].Value<string>();
        }
    }
}
