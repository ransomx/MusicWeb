using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicWeb.Utils
{
    public class Consts
    {
        // This is the URL the application will authenticate at.
        const string authString = "https://login.windows.net/hkrmusicapp.com";

        // These are the credentials the application will present during authentication
        // and were retrieved from the Azure Management Portal.
        const string clientID = "3856ce60-9388-4f46-a763-0bbb30df2132";
        const string clientSecret = "L2eOGX51vvJiRw47E2ZztKmZuLrKFcSunLZDt4n4POs=";

        // The Azure AD Graph API is the "resource" we're going to request access to.
        const string resAzureGraphAPI = "https://graph.windows.net";

        // The Azure AD Graph API for my directory is available at this URL.
        const string serviceRootURL = "https://graph.windows.net/hkrmusicapp.com";
    }
}