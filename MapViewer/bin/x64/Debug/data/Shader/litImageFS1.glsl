#version 330 core

in vec2 UV;
in vec4 col;

out vec4 color;

uniform sampler2D tDiffuse;
uniform sampler2D tShadow;

uniform int sShadow;

uniform vec3 lPos;
uniform vec3 lDif;
uniform vec3 lSpec;
uniform float lShiny;
uniform float lRange;

uniform float sWidth;
uniform float sHeight;

void main(){


  vec4 tc = texture2D(tDiffuse,UV);



    vec2 pos = gl_FragCoord.xy;
    
    pos.y = sHeight-pos.y;

    vec2 ss = vec2(pos.x/sWidth,pos.y/sHeight);


  

  
    vec2 lp = vec2(lPos.x,lPos.y);

    float xd = lp.x-pos.x;
    float yd = lp.y-pos.y;

    float dis = sqrt(xd*xd+yd*yd);

    dis = dis / lRange;


    if(dis>1.0)
    {

        dis = 1.0;

    }
    
    dis = 1.0-dis;

    float sv = texture2D(tShadow,ss).r;

     tc.xyz = tc.xyz * lDif * dis;

    if(sShadow==1){

     tc.xyz = tc.xyz * sv;

    }



    color  = tc;



}