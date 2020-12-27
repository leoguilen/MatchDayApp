<template>
  <div class="container">
    <header>
      <nav class="navbar">
        <div>
          <a class="navbar-brand" href="#">
            <img class="d-inline-block align-top" src="https://media.eadbox.com/system/uploads/medium/file/5d3cab6c0e3b5000422bc268/logo%20edools%20testehk2.png" alt="Logo" height="50">
          </a>
        </div>
        <div class="header-nav-bar-links">
          <a href="#">Suporte</a>
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
          <img height="500" src="https://th.bing.com/th/id/Rf310137ff80cddb1010ecd94f844ac76?rik=rTuDjO3j8%2fjZMQ&riu=http%3a%2f%2fwww.freepngclipart.com%2fdownload%2ffootball%2f84165-vector-player-football-players-free-frame.png&ehk=1WpfSdIjFPuVXRQeVw%2f9Hb69MIKldN%2fvUpE2UrKQS8o%3d&risl=&pid=ImgRaw" alt="">
        </div>
      </div>
    </section>
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
  .header-nav-bar-links a:nth-child(3){
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
</style>
