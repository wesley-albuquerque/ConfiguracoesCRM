Conta = {

    OnChanceCPF_CNPJ: function (executionContext) {
        var formContext = executionContext.getFormContext();

        formContext.getControl("naru_nomefantasia").setVisible(false);
        formContext.getControl("naru_inscricaoestadual").setVisible(false);
        formContext.getControl("naru_nomedocontato").setVisible(false);
        formContext.getAttribute("naru_nomefantasia").setValue("");
        formContext.getAttribute("naru_inscricaoestadual").setValue("");
        formContext.getAttribute("naru_nomedocontato").setValue("");

        var cpf = formContext.getAttribute("naru_cpf").getValue();
        if (cpf == null || (cpf.length != 11 && cpf.length != 14)) {
            formContext.ui.setFormNotification("CPF/CNPJ inválido", "INFO", "cpf/cnpj invalido")
            //formContext.getControl("naru_cpf").setNotification("CPF/CNPJ inválido");
            formContext.getAttribute("naru_cpf").setValue("");
            formContext.getControl("naru_cpf").setFocus();
            return
        }
        if (cpf.length == 14) {
            formContext.getControl("naru_nomefantasia").setVisible(true);
            formContext.getControl("naru_inscricaoestadual").setVisible(true);
            formContext.getControl("naru_nomedocontato").setVisible(true);
            formContext.getAttribute("naru_nomedocontato").setRequiredLevel("required");
            Conta.ConsultaCNPJ(executionContext);
        }
    },
    OnChanceCEP: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var cep = formContext.getAttribute("address1_postalcode").getValue();

        $.ajax({
            type: "GET",
            crossDomain: true,
            url: "https://viacep.com.br/ws/" + cep + "/json",
            async: false,
            dataType: "json",
            success: function (data) {
                var corpo = data;
            },
            error: function (data, exception, errorThrow) {
                var corpo = data;
            }
        });

        var reg = new RegExp([0 - 9]);
        cep.replace("-", "");
        var teste = reg.test(cep);
        if (cep.length == 8) {
            var req = new XMLHttpRequest();

            req.open("GET", encodeURI("https://viacep.com.br/ws/" + cep + "/json"), false);

            req.send(null);
            data = JSON.parse(req.responseText);

            formContext.getAttribute("address1_line1").setValue(data.logradouro);
            formContext.getAttribute("address1_line2").setValue(data.bairro);
            formContext.getAttribute("address1_city").setValue(data.localidade);
            formContext.getAttribute("address1_stateorprovince").setValue(data.uf);
            formContext.getAttribute("address1_country").setValue("Brasil");

        }
        else if (cep.length == 0) {
            return
        }
        else {
            alet("Cep inválido, digite novamente");
            formContext.getAttribute("address1_postalcode").setValue("");
            formContext.getControl("address1_postalcode").setFocus();
        }


    }, ConsultaCNPJ: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var cnpj = formContext.getAttribute("naru_cpf").getValue();

        var requisicao = new XMLHttpRequest()
        requisicao.open("GET", "https://api-publica.speedio.com.br/buscarcnpj?cnpj=" + cnpj, false);
        requisicao.send(null);
        var data = JSON.parse(requisicao.responseText);

        //$.ajax({
        //    type: "get",
        //    url: "https://receitaws.com.br/v1/cnpj" + cnpj,
        //    async: true,
        //    dataType: "json",
        //    crossDomain: true,
        //    contentType: "application/json" ,
        //    succes: function (data) {
        //        formcontext.getattribute("naru_nomefantasia").setvalue(data.fantasia);
        //        formcontext.getattribute("naru_inscricaoestadual").setvalue(data.atividade_principal[0].code)
        //    },
        //    error: function (data, exception, errorthrow) {
        //        var corpo = data;
        //    }
        //})

        formContext.getAttribute("naru_nomefantasia").setValue(data["RAZAO SOCIAL"]);
        formContext.getAttribute("naru_nomefantasia").setValue(data["NOME FANTASIA"]);
        formContext.getAttribute("naru_inscricaoestadual").setValue(data["CNAE PRINCIPAL CODIGO"]);
    }
        
}
