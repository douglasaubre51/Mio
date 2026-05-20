using System.Net.Http.Json;

namespace Mio.Services;

public class ProjectService
{
    private HttpClient _httpClient;
    private string _projectUri = EnvStore.MioBaseUrl + "/Project";

    public ProjectService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<ProjectModel>?> GetAll()
    {
        var result = await _httpClient.GetAsync($"{_projectUri}/all");
        if (result.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Getall Projects failed!");
            return null;
        }

        return await result.Content.ReadFromJsonAsync<List<ProjectModel>>();
    }

    public async Task<List<ProjectModel>> GetAllBookmarked()
    {
        var result = await _httpClient.GetAsync($"{_projectUri}/all/bookmarks");
        if (result.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("GetallBookmarks Projects failed!");
            return [];
        }

        return await result.Content.ReadFromJsonAsync<List<ProjectModel>>() ?? [];
    }
    public async Task<ProjectModel?> GetById(int projectId)
    {
        var result = await _httpClient.GetAsync($"{_projectUri}/{projectId}");
        if (result.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Getall Projects failed!");
            return null;
        }

        return await result.Content.ReadFromJsonAsync<ProjectModel>();
    }
    public async Task<bool> Add(ProjectModel project)
    {
        var result = await _httpClient.PostAsJsonAsync(
            $"{_projectUri}",
            project);
        if (result.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Getall Projects failed!");
            return false;
        }

        return true;
    }

    public async Task<bool> Update(ProjectModel project)
    {
        var response = await _httpClient.PutAsJsonAsync<ProjectModel>(
            $"{_projectUri}/{project.Id}",
            project);
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("project update failed!");
            return false;
        }

        return true;
    }
    public async Task<bool> Delete(int projectId)
    {
        var response = await _httpClient.DeleteAsync(
            $"{_projectUri}/{projectId}"
            );
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("project deletion failed!");
            return false;
        }

        return true;
    }

    public async Task<bool> AddBookmark(int projectId)
    {
        var response = await _httpClient.GetAsync(
            $"{_projectUri}/{projectId}/add-bookmark");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Add bookmark error: " + response.StatusCode);
            return false;
        }

        return true;
    }

    public async Task<bool> RemoveBookmark(int projectId)
    {
        var response = await _httpClient.GetAsync(
            $"{_projectUri}/{projectId}/remove-bookmark");
        if (response.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Remove bookmark error: " + response.StatusCode);
            return false;
        }

        return true;
    }

    public async Task<List<ProjectModel>> Search(string title)
    {
        var result = await _httpClient.GetAsync($"{_projectUri}/search/{title}");
        if (result.IsSuccessStatusCode is false)
        {
            Debug.WriteLine("Search Projects failed!");
            return [];
        }

        return await result.Content.ReadFromJsonAsync<List<ProjectModel>>() ?? [];
    }
}
