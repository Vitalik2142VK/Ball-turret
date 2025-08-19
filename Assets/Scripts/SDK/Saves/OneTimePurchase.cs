using System;

namespace YG
{
    [Serializable]
    public struct OneTimePurchase
    {
        public string Id;
        public bool IsPurchased;

        public OneTimePurchase(string id, bool isPurchased = false)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentOutOfRangeException(nameof(id));

            Id = id;
            IsPurchased = isPurchased;
        }
    }
}