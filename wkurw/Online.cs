using Android.Content;
using Android.Net;
using Plugin.Connectivity;
using System.Net;

namespace wkurw
{
    public class Online
    {
        public bool is_Online()
        {
            if (CrossConnectivity.Current.IsConnected) return true;
            else return false;
        }
        
    }
}