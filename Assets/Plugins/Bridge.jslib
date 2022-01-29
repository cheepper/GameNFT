mergeInto(LibraryManager.library, {
    Login: function () {
        login();
    },
    AmountToMint: function (amount) {
        mint(amount);
    },
    AmountToBurn: function (amount) {
        burn(amount);
    },
    AmountToTransfer: function (address, amount) {
        transfer(Pointer_stringify(address), amount);
    },
    AmountOfSupply: function (amount) {
        supply(amount);
    },
    AmountToClaim: function (amount) {
        claim(amount);
    },
    AmountToSpend: function (amount) {
        spend(amount);
    },
    RefreshBalance: function () {
        refresh();
    },
});