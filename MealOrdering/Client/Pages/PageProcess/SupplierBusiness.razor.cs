using Blazored.LocalStorage;
using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrdering.Client.Pages.PageProcess
{
    public class Supplier : ComponentBase
    {

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorageSync { get; set; }

        [Inject]
        ModalManager ModalManager { get; set; }

        [Inject]
        IJSRuntime jsRuntime { get; set; }



        protected List<SupplierDTO> SupplierList;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadList();
        }

        public void GoCreateSupplier()
        {
            UrlNavigationManager.NavigateTo("/suppliers/add");
        }

        public void GoEditOrder(Guid SupplierId)
        {
            UrlNavigationManager.NavigateTo("/suppliers/edit/" + SupplierId.ToString());

        }

        public async Task ReLoadList()
        {
            var res = await Http.GetFromJsonAsync<ServiceResponse<List<SupplierDTO>>>($"api/Supplier/Suppliers");

            SupplierList = res.Success && res.Value != null ? res.Value : new List<SupplierDTO>();
        }

        public async Task DeleteSupplier(Guid SupplierId)
        {
            var modalRes = await ModalManager.ConfirmationAsync("Confirm", "Supplier will be deleted. Are you sure?");
            if (!modalRes)
                return;

            try
            {
                var res = await Http.PostGetBaseResponseAsync("api/Supplier/DeleteSupplier", SupplierId);

                if (res.Success)
                {
                    SupplierList.RemoveAll(i => i.Id == SupplierId);
                    //await loadList();
                }
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Error", ex.Message);
            }
        }

        public async void GoWebUrl(Uri Url)
        {
            await jsRuntime.InvokeAsync<object>("open", Url.ToString(), "_blank");
        }
    }
}
