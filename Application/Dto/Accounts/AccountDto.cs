﻿namespace Application.Dto.Accounts
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
    }
}
