using System;
using System.IO;
using Vosk;
using System.Speech;
using System.Speech.Recognition;

namespace BeHeardSpeechRecognitionAPI.Application
{
    public class SpeechRecognition
    {
        public static string Predict(string base64)
        {
            Vosk.Vosk.SetLogLevel(-1);

            // TODO: Starting the model at entry point asynchronously may improve speed
            var currentDirectory = Directory.GetCurrentDirectory();
            var srModel = System.IO.Path.Combine(currentDirectory, @"SRModel");
            var modelPath = Path.GetFullPath(srModel);
            var model = new Model(modelPath);
            VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);

            byte[] speechData = Convert.FromBase64String(base64);
            using (MemoryStream ms = new MemoryStream(speechData))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        var result = rec.Result();
                    }
                    else
                    {
                        var result = rec.PartialResult();
                    }
                }
            }
            var prediction = rec.FinalResult();
            return prediction;
        }
    }

}
