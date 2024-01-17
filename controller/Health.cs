using Microsoft.AspNetCore.Mvc;
using model.ResponseModel;

namespace controller.Health;

[Route("api/[controller]")]
[ApiController]
public class Controller : ControllerBase{

    [HttpGet]
    public IActionResult HealthCheck(){
        var res = new ResponseModel();
        var healthStatus = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow
        };
        return Ok(res.SuccessWithData(healthStatus));
    }
}