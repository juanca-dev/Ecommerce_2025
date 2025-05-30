using Microsoft.JSInterop;

namespace Ecommerce.Frontend.Helpers
{
    public static class IJSExtensions
    {
        //guardar el token en el local storage
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
             => js.InvokeAsync<object>(
                 "localStorage.setItem",
                 key, content
                 );

        //obtener el token del local storage
        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        //eliminar el token del local storage
        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
           => js.InvokeAsync<object>(
               "localStorage.removeItem",
               key);
    }
}