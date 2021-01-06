<template>
  <div class="container">
    <header>
      <nav class="navbar">
        <div>
          <a class="navbar-brand" href="#">
            <img class="d-inline-block align-top" src="../../assets/images/logo.png" alt="Logo" height="50">
          </a>
        </div>
        <div class="header-nav-bar-links">
          <a href="#">Suporte</a>
          <a href="#">Baixar</a>
          <span style="font-weight: 300">|</span>
          <a href="#">Inscrever-se</a>
          <a href="#">Entrar</a>
        </div>
      </nav>
    </header>

    <section class="section1">
      <div class="row">
        <div class="col-6 text-left">
          <p style="font-size: 15px; letter-spacing: 2px"><b>MATCH DAY APP</b></p>
          <h1 style="font-size: 60px"><b>Tá querendo marcar aquele futebol com a galera?</b></h1>
          <p style="letter-spacing: 0.5px"><b>Agora com o MatchDayApp nós agilizamos isso para você!</b></p>
          <p style="letter-spacing: 0.5px"><b>Nós buscamos partidas e times adversários que queiram jogar na mesma data, hora e local.</b></p>
          <button type="button">COMECE JÁ</button>
          <p style="font-size: 13px; margin-top: 2.5rem; margin-bottom: -1rem">O uso desse aplicativo é totalmente isento de custos. Para mais informações consulte os Termos e Condições de Uso.</p>
        </div>
        <div class="col-6">
          <img height="500" src="" alt="Futebol">
        </div>
      </div>
    </section>
    <section class="section2">
      <div class="row">
        <div class="col-6">
          <img height="500" src="../../assets/images/mobile.png" alt="Smartphone">
        </div>
        <div class="col-6 text-right">
          <p style="font-size: 15px; letter-spacing: 2px"><b>MATCH DAY APP MOBILE</b></p>
          <h1 style="font-size: 60px"><b>Aplicativo dispónivel para download</b></h1>
          <p style="letter-spacing: 0.5px;font-weight: 100;"><b>Marque suas partidas aonde estiver com apenas um <br> toque na tela.</b></p>
          <button type="button">Baixe agora o app</button>
        </div>
      </div>
    </section>
    <footer class="text-center text-lg-start">
      <div class="text-center p-3">
        <div class="footer-logo">
          <a class="navbar-brand" href="#">
            <img class="d-inline-block align-top" src="../../assets/images/logo.png" alt="Logo" height="50">
          </a>
        </div>
      </div>
      <div class="text-center">
        <div class="footer-socialmedias">
          <a href="https://www.instagram.com/leocruzg_" target="_blank">
            <span><i class="fab fa-instagram"></i></span>
          </a>
          <a href="https://www.linkedin.com/in/leonardo-guilen/" target="_blank">
            <span><i class="fab fa-linkedin"></i></span>
          </a>
          <a href="https://github.com/leoguilen" target="_blank">
            <span><i class="fab fa-github"></i></span>
          </a>
        </div>
      </div>
      <div class="text-center p-3">
        <div class="footer-links">
          <a href="#" target="_blank">Sobre</a>
          <a href="https://github.com/leoguilen/matchday-app">Source code</a>
          <a href="#" target="_blank">Inscrever-se</a>
          <a href="#" target="_blank">Baixar</a>
        </div>
      </div>
      <div class="text-center p-3">
        <div class="footer-copyright">
          © 2021 Copyright: MatchDayApp Team
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

<style scoped lang="scss">
.container {
  margin: 0;
  padding: 0;
}
header {
  .navbar {
    width: 100vw;
    height: 75px;
    display: inline-flex;
    align-items: center;
    justify-content: space-between;
    background: var(--header);
    color: var(--white);
    font-weight: 700;
    padding: 0 15rem 0 15rem;
  }
  .navbar-brand img {
    cursor: pointer;
  }
  .header-nav-bar-links > a{
    color: var(--white);
    text-decoration: none;
    margin: 0 2.5rem 0 0;
  }
  .header-nav-bar-links a:nth-child(4){
    margin-left: 2.5rem;
    color: #CBDADC;
  }
  .header-nav-bar-links a:last-child{
    color: #CBDADC;
  }
  .header-nav-bar-links a:hover{
    color: #1DB954;
  }
}

section {
  width: 100vw;
  padding: 0 15rem 0 16rem;
}

.section1 {
  background: #A5FFEF;
  color: #CB1582;
  padding-top: 5rem;
  padding-bottom: 5rem;

  p {
    font-size: 17px;
  }

  button {
    width: 18rem;
    height: 2.7rem;
    border-radius: 20px;
    background: #CB1582;
    color: white;
    font-weight: 700;
    letter-spacing: 1px;
    margin-top: 20px;
  }

  button:hover {
    background: white;
    color: black;
  }
}
.section2 {
  background: #2D46B9;
  color: #1ED760;
  padding-top: 5rem;
  padding-bottom: 5rem;

  p {
    font-size: 17px;
  }

  button {
    width: 18rem;
    height: 2.7rem;
    border-radius: 20px;
    background: #1ED760;
    color: #2D46B9;
    font-weight: 700;
    letter-spacing: 1px;
    margin-top: 20px;
  }

  button:hover {
    background: white;
    color: black;
  }
}

footer {
  width: 100vw;
  padding: 2rem 15rem 2rem 16rem;
  background: var(--header);
  color: var(--white);

  .footer-socialmedias {
    margin-left: 2.5rem;
  }

  .footer-links {
    margin-left: 2.5rem;
  }

  .footer-socialmedias a > span{
    color: var(--white);
    font-size: 2.5em;
    text-decoration: none;
    margin: 0 2.5rem 0 0;
  }

  .footer-socialmedias a span:hover{
    color: #1DB954;
  }

  .footer-links > a {
    color: var(--white);
    text-decoration: none;
    margin: 0 2.5rem 0 0;
  }

  .footer-links a:hover{
    color: #1DB954;
  }

  .footer-copyright {
    font-size: 0.8rem;
    letter-spacing: 1.2px;
    opacity: 0.75;
  }
}

</style>
