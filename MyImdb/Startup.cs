using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(MyImdb.Startup))]
namespace MyImdb {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}