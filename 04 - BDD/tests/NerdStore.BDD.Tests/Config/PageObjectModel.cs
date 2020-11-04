namespace NerdStore.BDD.Tests.Config
{
    //Classe que representa um repositório genérico, com métodos comuns no SeleniumHelper.
    public abstract class PageObjectModel
    {
        protected readonly SeleniumHelper Helper;

        protected PageObjectModel(SeleniumHelper helper)
        {
            Helper = helper;
        }

        public string ObterUrl()
        {
            return Helper.ObterUrl();
        }

        public void NavegarParaUrl(string url)
        {
            Helper.IrParaUrl(url);
        }
    }
}