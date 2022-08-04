using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        public string Invoke()
        {
            return "Hello from the Nav View Component";
        }
    }
}
