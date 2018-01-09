using System;
using System.Collections.Generic;
using System.Text;

namespace BennysApp
{
    public interface ITextToSpeech
    {
        void Speak(string text);
    }
}
