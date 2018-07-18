using Airport.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Airport.BLL
{
    public static class LogWriter
    {
        public static async Task WriteCrewsToFileAsync(string filePath, List<Crew> crews)
        {
            var fileName = $"log_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";

            using (FileStream stream = new FileStream(filePath + fileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(stream))
            {
                await sw.WriteLineAsync($"CrewId, PilotId");
                
                foreach (var crew in crews)
                {
                    await sw.WriteLineAsync($"{crew.Id}, {crew.Pilot.Id}");
                }
            }
        }
    }
}
