namespace Gadget_Gourmet.Models.Interface
{
    public interface IHistory
    {
        public void TrackPageVisit(string pageName, string pageUrl , string pageTime);
    }
}
