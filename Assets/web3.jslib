mergeInto(LibraryManager.library, {
    WalletAddress: function () {
        var returnStr = "";

        try {
            // get address from metamask
            returnStr = ethereum.selectedAddress;

        } catch (e) {
            returnStr = "";
        }
        var returnStr = ethereum.selectedAddress;
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize)
        console.log("buffer from jslib address:");
        console.log(`${buffer}`);
        return buffer;
    },


    EthBalance: async function () {
        var ethBalance = "";

        async function GetBalanceMethod() {
            let res = ethereum.request({ method: "eth_getBalance", params: [ethereum.selectedAddress, "latest"] }).then(function (res) {
                ethBalance = (parseInt(res) / (10 ** 18)).toString();
                console.log("inside function");
                console.log(ethBalance);
                return ethBalance;
            })
            return res;
        }

        ethBalance = await GetBalanceMethod();

        console.log("balance from jslib:");
        console.log(`${ethBalance}`);
        ethBalance = `${ethBalance}`;

        var bufferSize = lengthBytesUTF8(ethBalance) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(ethBalance, buffer, bufferSize);

        console.log("buffer from jslib balance:");
        console.log(`${buffer}`);
        return buffer;

        //return ethBalance;
    }

});

//<div class="AssetPagereact__DivContainer-sc-119bh74-0 fQBVap">
//<div class="OrderManagerreact__DivCotainer-sc-rw3i3h-0 PpvMj">
//<div class="OrderManager--cta-container">
//<span>
//<a class="styles__StyledLink-sc-l6elh8-0 ekTmzq Blockreact__Block-sc-1xf18x6-0 Buttonreact__StyledButton-sc-glfma3-0 fuGyEk kCijbX OrderManager--second-button" width="162px" href="/">Sell</a>
//</span>
//</div>
//</div>
//</div>