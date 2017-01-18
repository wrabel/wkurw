using Refractored.Xam.TTS;
using Refractored.Xam.TTS.Abstractions;

namespace wkurw
{
    public class TexTtoSpeecH
    {
        CrossLocale ttm = new CrossLocale();

        public TexTtoSpeecH()
        {
            var tmp = CrossTextToSpeech.Current.GetInstalledLanguages();
            foreach (var var in tmp)
            {
                if (var.Language.ToString() == "pl")
                {
                    ttm = var;
                    break;
                }
            }
        }
        public void say_(string text)
        {
            CrossTextToSpeech.Current.Speak(text, false, ttm);
        }
    }
}