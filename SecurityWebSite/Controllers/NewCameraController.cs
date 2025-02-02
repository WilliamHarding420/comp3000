﻿using Microsoft.AspNetCore.Mvc;
using SecurityWebSite.DatabaseModels;

namespace SecurityWebSite.Controllers
{
    [Route("/cctv/new")]
    [ApiController]
    public class NewCameraController : Controller
    {

        public struct CameraData
        {
            public string Name { get; set; }
            public string IP { get; set; }
            public string Port { get; set; }
            public string PublishURL { get; set; }
            public int? LocationID { get; set; }
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<string> AddCamera([FromBody] CameraData cameraData)
        {

            int locationID = (cameraData.LocationID == null) ? 0 : (int) cameraData.LocationID;

            Camera camera = new Camera()
            {
                Name = cameraData.Name,
                IP = cameraData.IP,
                Port = cameraData.Port,
                PublishURL = cameraData.PublishURL,
                LocationID = locationID
            };
            Database db = new();

            await db.Cameras.AddAsync(camera);
            await db.SaveChangesAsync();

            return await JsonResponse<string>.SingleResponse("success", "Camera was successfully added.");

        }

    }
}
