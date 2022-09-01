Produto = {
    OnLoad: function (executionContext) {
        formContext = executionContext.getFormContext();

        formContext.getControl('parentproductid').setVisible(false)

    }    
}