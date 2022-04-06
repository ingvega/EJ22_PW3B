/*var alumno={
    'nombre':'Juan Perez',
    'carrera':Sistemas,
    'semestre':1,
    materias:[
        {
            nombre:'matematicas',
            calif:90
        },
        {
            nombre:'español',
            calif:100
        }
    ]    
};

alumno.nombre*/


var letras=document.getElementsByClassName('letra');

/*letras[0].animate(
    [{keyframes}],
    {configuración de la animación}
);*/
letras[0].animate(
    [
        //0% o from
        { transform:'rotateY(180deg)', backgroundColor:'white' },
        //100% o to
        { transform:'rotateY(0deg)' , backgroundColor:'#ddd' }
    ],
    {
        duration:2000,
        delay:1000,
        fill: 'forwards'
    }
);
