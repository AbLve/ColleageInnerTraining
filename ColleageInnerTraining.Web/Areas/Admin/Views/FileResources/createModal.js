(function () {
    
    appModule.controller('host.views.fileResources.createModal', [
        '$scope', '$uibModalInstance','$sce',
        function ($scope, $uibModalInstance, $sce) {
            var vm = this;
            vm.requestParams =
                {
                    apiUrl: "",
                    timestamp: null,
                    appkey: null,
                    sign: null,
                    apiUrl: null
                };
            //$scope.apiUrl = $sce.trustAsResourceUrl(vm.requestParams.apiUrl); //URL 为全链接（$sce.trustAsResourceUrl("http://" + url)）
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };


            vm.loadSetting = function () {
                vm.loading = true;
                abp.ajax({
                    type: 'GET',
                    url: abp.appPath + 'commSetting',
                    data: { type: "FileUpload" }
                }).done(function (result) {
                    $scope.apiUrl = $sce.trustAsResourceUrl(result.url + "?sTimestamp=" + result.timestamp + "&appkey=" + result.appkey + "&sign=" + result.sign + "&type=courseware");
                })
                vm.loading = false;
            }
            vm.loadSetting();

        }
    ]);
})();