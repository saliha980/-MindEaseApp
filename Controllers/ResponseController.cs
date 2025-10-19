using MentalEase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MentalEase.Controllers
{
    public class ResponseController : Controller
    {
        private readonly string _storagePath = "responses.json";

        [HttpPost]
        public IActionResult SaveResponse([FromBody] ResponseModel response)
        {
            try
            {
                // Read existing responses
                var responses = new List<ResponseModel>();
                if (System.IO.File.Exists(_storagePath))
                {
                    var json = System.IO.File.ReadAllText(_storagePath);
                    responses = JsonSerializer.Deserialize<List<ResponseModel>>(json) ?? new List<ResponseModel>();
                }

                // Add new response with ID
                response.Id = responses.Count + 1;
                responses.Add(response);

                // Save back to file
                var options = new JsonSerializerOptions { WriteIndented = true };
                var newJson = JsonSerializer.Serialize(responses, options);
                System.IO.File.WriteAllText(_storagePath, newJson);

                return Ok(new { success = true, message = "Response saved!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetResponses()
        {
            try
            {
                if (System.IO.File.Exists(_storagePath))
                {
                    var json = System.IO.File.ReadAllText(_storagePath);
                    var responses = JsonSerializer.Deserialize<List<ResponseModel>>(json);
                    return Json(responses);
                }
                return Json(new List<ResponseModel>());
            }
            catch (Exception)
            {
                return Json(new List<ResponseModel>());
            }
        }

        [HttpGet]
        public IActionResult ViewResponses()
        {
            return View();
        }
    }
}