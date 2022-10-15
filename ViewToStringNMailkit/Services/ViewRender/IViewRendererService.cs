using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViewToStringNMailKit.Services.ViewRender
{
    public interface IViewRendererService
    {
        Task<string> RenderAsync<TModel>(Controller controller, string name, TModel model);
    }
}
