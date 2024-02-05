using System.Net.Http.Json;
using System.Text.Json;

public class ShiftService
{
    private readonly HttpClient _httpclient = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:5273/")
    };

    public async Task<List<Shift>?> GetAll()
    {
        List<Shift>? shiftList = await _httpclient.GetFromJsonAsync<List<Shift>>("shifts");
        return shiftList;
    }

    public async Task<Shift?> GetOne(int id)
    {
        Shift? shift = await _httpclient.GetFromJsonAsync<Shift>($"shifts/{id}");
        return shift;
    }

    public async Task<bool> Create(Shift newShift)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(newShift));
        var response = await _httpclient.PostAsync("shifts", jsonContent);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpclient.DeleteAsync($"shifts/{id}");
        return response.IsSuccessStatusCode;
    }
}