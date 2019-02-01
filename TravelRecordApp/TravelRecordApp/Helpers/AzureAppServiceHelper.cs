#define OFFLINE_SYNC_ENABLED

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TravelRecordApp.Helpers
{
    public class AzureAppServiceHelper
    {
        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> SyncErrors = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();
                await App.postsTable.PullAsync("userPosts", "");
            }
            catch (MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                {
                    SyncErrors = mspfe.PushResult.Errors;
                }
            }
            catch (Exception ex) { }

            if (SyncErrors != null)
            {
                foreach (var syncError in SyncErrors)
                {
                    if (syncError.OperationKind == MobileServiceTableOperationKind.Update && syncError.Result != null)
                    {
                        await syncError.CancelAndUpdateItemAsync(syncError.Result);
                    }
                    else
                    {
                        await syncError.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

    }
}
