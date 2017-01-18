using Android.Widget;

namespace wkurw
{
    interface IOnline
    {
        void ChceckNetwork();
        void ZmienKolor();
        void Lang_switch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e); // jest tutaj poniewaz klasa potrzebuje dostepu do interneru
    }
}