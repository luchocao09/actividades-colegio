// MOSTRAR EL LISTADO DE CLIENTES VIP
// MOSTRAR EL LISTADO DE LOS CLIENTES QUE NO TENGAN CARGO EXTRA
//ASIGNAR CARGO EXTRA EN TRUE A LOS CLIENTES QUE TENGAN MEMBRESIA BLACK
//SE PUEDE HACER CON UN ARRAY O CON UN JSON

let botn = document.getElementById("boton");
botn.addEventListener("click", procesarClientes)

async function procesarClientes() {

  try {

    const rta = await fetch("data.json")
    const clientes = await rta.json()


    const conblacky = clientes.map(e => {
      if (e.membresia == "Black") {
        let jonson = true;
        return {
          ...e,
          cargoExtra: jonson
        };
      }
      else
        return { ...e };



    })
    // mostrar todos

    const tablaBody = document.getElementById("cli_vip")

    const filas = conblacky.map(e => `
    
    <tr>
    <td>${e.id}</td>
    <td>${e.nombre}</td>
    <td>${e.membresia}</td>
      <td>${e.cargoExtra}</td>

    </tr>
    
    `)


    tablaBody.innerHTML = filas.join("")







    const tablaBody2 = document.getElementById("cli_sin_extra")
    const tBody2 = sincargini.map(e => {
      if (e.cargoExtra==false) {
        `
  
  <tr>
  <td>${e.id}</td>
  <td>${e.nombre}</td>
  <td>${e.membresia}</td>
  <td>${e.cargoExtra}</td>
  </tr>
  
  `
      }
    })

    tablaBody2.innerHTML = tBody2.join("")

  }
  catch (error) {
    console.log("error " + error)
  }

}
