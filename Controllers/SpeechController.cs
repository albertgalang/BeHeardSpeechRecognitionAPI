using BeHeardSpeechRecognitionAPI.Application;
using Microsoft.AspNetCore.Mvc;

namespace BeHeardSpeechRecognitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        // POST api/<SpeechController>
        [Route("recognizer")]
        [HttpPost]
        public string SpeechToText([FromBody] string value)
        {
            var result = SpeechRecognition.Predict(value);

            return result; // should be a json string
        }
    }
}
