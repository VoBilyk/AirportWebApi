using Airport.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace Airport.BLL
{
    public static class LogWritter
    {
        public static async Task WriteCrewsToFileAsync(string filePath, List<Crew> crews)
        {
            var fileName = $"log_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";

            using (FileStream stream = new FileStream(filePath + fileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(stream))
            {                
                foreach (var crew in crews)
                {
                    await sw.WriteLineAsync($"CrewId: {crew.Id}, PilotId: {crew.Pilot.Id}, Number of Stewardess: {crew.Stewardesses.Count}");
                }
            }
        }
    }
}
