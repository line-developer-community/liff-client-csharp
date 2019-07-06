window.liffInterop = {
    init: function () {
        return new Promise(function (resolve, reject) {
            liff.init(
                function (data) {
                    resolve(JSON.stringify(data));
                },
                function (error) {
                    reject(error);
                });
        });
    },
    alert: function (message) {
        alert(message);
    }
};