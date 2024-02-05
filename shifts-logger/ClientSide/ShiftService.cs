using System.Net.Http.Json;
using System.Text.Json;

public class ShiftService
{
    private readonly HttpClient _httpclient = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:5273/")
    };

    public async Task<List<Shift>?> GetAllShifts()
    {
        List<Shift>? shiftList = await _httpclient.GetFromJsonAsync<List<Shift>>("shifts");
        return shiftList;
    }

    public async Task<Shift?> GetOneShift(int id)
    {
        Shift? shift = await _httpclient.GetFromJsonAsync<Shift>($"shifts/{id}");
        return shift;
    }

    public async Task PostShift(Shift newShift)
    {
        return;
    }
}