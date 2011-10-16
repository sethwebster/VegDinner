using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId;

namespace NerdDinner
{
    public static class RealmExtensions
    {
        public static Realm AutoResolve(this Realm input)
        {
            var stringRes = "";
            if (input.Host.ToLower() != "localhost")
            {
                stringRes = input.Scheme + "://" + input.Host + input.PathAndQuery;
                var realmRet = new Realm(
                        stringRes
                    );
                return realmRet;
            }
            else
            {
                return input;
            }
        }
    }
}