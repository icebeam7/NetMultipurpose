using Microsoft.Extensions.ML;
using Microsoft.AspNetCore.Mvc;

using TaxiFarePrediction.Models;

namespace TaxiFarePrediction.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictController : ControllerBase
    {
        private readonly PredictionEnginePool<TaxiTrip, TaxiTripFarePrediction> _predictionEnginePool;

        public PredictController(PredictionEnginePool<TaxiTrip, TaxiTripFarePrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost]
        public ActionResult Post([FromBody] TaxiTrip input)
        {
            TaxiTripFarePrediction prediction = _predictionEnginePool.Predict(modelName: "TaxiTripFarePredictionModel", example: input);

            return Ok(prediction.FareAmount);
        }
    }
}
