namespace PetDream.Domain.Validation
{
    public static class ValidateCPF
    {
        public static bool IsValid(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int left;

            var currentCpf = cpf.Trim();
            
            currentCpf = currentCpf.Replace(".", "").Replace("-", "");
            
            if (currentCpf.Length != 11)
                return false;
            
            tempCpf = currentCpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            
            left = sum % 11;
            
            left = left < 2 ? 0 : 11 - left;

            digit = left.ToString();

            tempCpf = tempCpf + digit;
            sum = 0;
            
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            
            left = sum % 11;
            
            left = left < 2 ? 0 : 11 - left;

            digit = digit + left.ToString();
            
            return currentCpf.EndsWith(digit);
        }

    }
}