Oportunidade = {
    OnchangeConta: function (executionContext) {
        formContext = executionContext.getFormContext();

        if (formContext.getAttribute("parentaccountid").getValue() != null) {
            var accountid = formContext.getAttribute("parentaccountid").getValue()[0].id;

            Xrm.WebApi.retrieveRecord("account", accountid, "?$select=naru_cpf").then(
                function success(result) {
                    formContext.getAttribute("naru_cnpjcpf").setValue(result.naru_cpf);

                },
                function (error) {
                    alert(error.message)
                });


        } else
            formContext.getAttribute("naru_cnpjcpf").setValue("");
    }

        
}