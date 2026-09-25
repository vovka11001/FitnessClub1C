namespace FitnessClub.Orders
{
    internal class Order
    {
        private readonly string _customerFullName;
        private readonly string _customerPhone;
        private readonly string _membershipTypeName;

        public Order(string customerFullName, string customerPhone, string membershipTypeName)
        {
            if (string.IsNullOrWhiteSpace(customerFullName))
            {
                throw new ArgumentException("ФИО клиента не может быть пустым.", nameof(customerFullName));
            }

            if (string.IsNullOrWhiteSpace(customerPhone))
            {
                throw new ArgumentException("Телефон клиента не может быть пустым.", nameof(customerPhone));
            }

            if (string.IsNullOrWhiteSpace(membershipTypeName))
            {
                throw new ArgumentException("Название вида абонемента не может быть пустым.", nameof(membershipTypeName));
            }

            _customerFullName = customerFullName;
            _customerPhone = customerPhone;
            _membershipTypeName = membershipTypeName;
        }

        public string CustomerFullName => _customerFullName;
        public string CustomerPhone => _customerPhone;
        public string MembershipTypeName => _membershipTypeName;
    }
}