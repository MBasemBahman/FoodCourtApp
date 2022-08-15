namespace Entities.EnumData
{
    public static class DBModelsEnum
    {
        public enum OpenTypeEnum
        {
            Account = 1,
            Order = 2,
            OrderStateHistory = 3,
            Payment = 4,
            PaymentStateHistory = 5,
            CarReminder = 6
        }
        public enum ReceivingStatusEnum
        {
            Before = 1,
            After = 2
        }

        public enum AccountTypeEnum
        {
            Customer = 1,
            Employee = 2
        }

        public enum OrderStateEnum
        {
            Pending = 1,
            Canceled = 2,
            Done = 3
        }
        public enum PaymentPurposeEnum
        {
            Orders = 1,
            Custody = 2,
            Bills = 3,
            Purchases = 4,
            Ancestor = 5,
            Salary = 6
        }
        public enum PaymentStateEnum
        {
            Pending = 1,
            Done = 2,
            NotDelivered = 3,
            Refunded = 4,
            Canceled = 5
        }
        public enum PaymentTypeEnum
        {
            Import = 1,
            Export = 2
        }
        public enum PaymentWayEnum
        {
            Cash = 1,
            BankTransfer = 2,
            VisaCard = 3
        }
        public enum ReminderTypeEnum
        {
            Infractions = 1,
            Liquids = 2,
            Parts = 3
        }
    }
}
