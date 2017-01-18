using Plugin.Connectivity;

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