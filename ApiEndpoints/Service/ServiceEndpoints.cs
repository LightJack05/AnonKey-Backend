namespace KeyShare_Backend.ApiEndpoints.Service;

public static class ServiceEndpoints{
	public static void MapEndpoints(WebApplication app){
		app.MapGet("/service/ping", Ping.GetPing);
	}	 

}
