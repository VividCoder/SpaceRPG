#version 330 core

in vec2 UV;
in vec4 col;
in float iZ;

out vec4 color;

uniform sampler2D tDiffuse;
uniform sampler2D tNormal;
uniform sampler2D tShadow;

uniform int sShadow;

uniform vec2 Resolution;
uniform vec3 LightPos;
uniform vec4 LightColor;
uniform vec4 AmbientColor;
uniform vec3 Falloff;


void main(){


    vec4 DiffuseColor = texture2D(tDiffuse,UV);

    vec3 NormalMap = texture2D(tNormal,UV).rgb;

    //NormalMap.g = 1.0 - NormalMap.g;

    vec3 LightDir = vec3(LightPos.xy - (gl_FragCoord.xy / Resolution.xy), LightPos.z);
	
    LightDir.x *= Resolution.x / Resolution.y;

    float D = length(LightDir);

    vec3 N = normalize(NormalMap * 2.0 - 1.0);
	vec3 L = normalize(LightDir);
	
    vec3 Diffuse = (LightColor.rgb * LightColor.a) * max(dot(N, L), 0.0);

    vec3 Ambient = AmbientColor.rgb * AmbientColor.a;
	
    float Attenuation = 1.0 / ( Falloff.x + (Falloff.y*D) + (Falloff.z*D*D) );
	
    
    vec3 Intensity = Ambient + Diffuse * Attenuation;
	vec3 FinalColor = DiffuseColor.rgb * Intensity;

    color =  vec4(FinalColor, DiffuseColor.a);


    //color  = tc;




}