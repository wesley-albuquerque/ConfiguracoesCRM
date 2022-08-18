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
        if (cpf == null || cpf.length < 11 || (cpf.length > 11 && cpf.length < 14) || cpf.length > 14) {
            alert("CPF/CNPJ inválido")
            //formContext.getControl("naru_cpf").setNotification("CPF/CNPJ inválido");
            formContext.getAttribute("naru_cpf").setValue("");
            formContext.getControl("naru_cpf").setFocus();
            return
        }
        if (cpf.length == 14) {
            formContext.getControl("naru_nomefantasia").setVisible(true);
            formContext.getControl("naru_inscricaoestadual").setVisible(true);
            formContext.getControl("naru_nomedocontato").setVisible(true);
        }
    }
}
