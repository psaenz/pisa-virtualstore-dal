using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class OrderStatuses : BaseStatus
    {
        public readonly GeneralStatus PENDING;
        public readonly GeneralStatus PROCESSING;
        public readonly GeneralStatus DELIVERING;
        public readonly GeneralStatus DELIVERED;
        public readonly GeneralStatus CANCELLED;

        private static OrderStatuses _instance = null;

        private OrderStatuses() : base("Order")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                PENDING = GetStatus(context, "Pending");
                PROCESSING = GetStatus(context, "Processing");
                DELIVERING = GetStatus(context, "Delivering");
                DELIVERED = GetStatus(context, "Delivered");
                CANCELLED = GetStatus(context, "Cancelled");
            }
        }

        public static OrderStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new OrderStatuses();
            return _instance;
        }
    }
    }
