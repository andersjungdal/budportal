namespace BlazorBusinessLogic.ApiConnections
{
    public class UriGenneratore
    {
        public static string GennreadURL(string Url)
        {
            if (Url.Contains('?'))
            {
                return Url + "&code=KWHxOB7tVxhLGSDR3kI1/tywVIuN68RnHvOrlGK4cvRMk4Lcml58nw==";
            }
            return Url + "?code=KWHxOB7tVxhLGSDR3kI1/tywVIuN68RnHvOrlGK4cvRMk4Lcml58nw==";

        }
    }
}