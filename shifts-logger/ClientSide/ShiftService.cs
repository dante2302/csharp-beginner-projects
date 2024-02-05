using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
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
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(newShift),
            encoding: Encoding.UTF8,
            mediaType: MediaTypeHeaderValue.Parse("application/json")
        ); 

        var response = await _httpclient.PostAsync("shifts", jsonContent);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Edit(int id, Shift editedShift)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(editedShift),
            encoding: Encoding.UTF8,
            mediaType: MediaTypeHeaderValue.Parse("application/json")
            );

        Printer.PrintShifts([editedShift]);
        var response = await _httpclient.PutAsync($"shifts/{id}", jsonContent);
        Console.WriteLine(response.StatusCode);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpclient.DeleteAsync($"shifts/{id}");
        return response.IsSuccessStatusCode;
    }
}