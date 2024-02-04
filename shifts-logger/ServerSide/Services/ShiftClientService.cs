namespace shifts_logger.Services
{
    public class ShiftClientService
    {
        private readonly string baseUrl = $"{Configuration.Manager["applicationUrl"]}/shifts";
        private readonly HttpClient _client = new HttpClient();

        public async Task GetAllShifts()
        {
            var response = await _client.GetAsync(baseUrl);
            Console.WriteLine(response);
        }
    }
}
