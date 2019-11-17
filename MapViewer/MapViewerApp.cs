using Vivid.App;

namespace MapViewer
{
    public class MapViewerApp : VividApp
    {

        public MapViewerApp() : base("SpaceRPG - Map Viewer", 1370, 768, false)

        {

        }

        public static void InitViewer()
        {

            var App = new MapViewerApp();
            InitState = new MapViewer.States.TileSetEditor();
            App.Run();
            return;


        }
    }
}
