using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminho_Plataformas : MonoBehaviour
{
    public List<GameObject> plataformasAtivas;
    public List<GameObject> plataformasDesativas;


    public void DesativarTudo()
    {
        for (int i = 0; i < plataformasAtivas.Count; i++)
        {
            if (plataformasAtivas[i].TryGetComponent(out React_Plataforma reactplataforma))
            {
                reactplataforma.Desativar();
            }
            else
            {
                if (plataformasAtivas[i].TryGetComponent(out Loop_Plataforma loopPlataforma))
                {
                    loopPlataforma.Desativar();
                }
                else
                {
                    if (plataformasAtivas[i].TryGetComponent(out Fade_Plataforma fadePlataforma))
                    {
                        fadePlataforma.Desativar();
                    }
                    else
                    {
                        if (plataformasAtivas[i].TryGetComponent(out Shake_Plataforma shakePlataforma))
                        {
                            shakePlataforma.Desativar();
                        }
                    }
                }
            }
        }
    }

    public void AtivarTudo()
    {
        for (int i = 0; i < plataformasDesativas.Count; i++)
        {
            if (plataformasAtivas[i].TryGetComponent(out Shake_Plataforma shakePlataforma))
            {
                shakePlataforma.Desativar();
            }
            else
            {
                if (plataformasDesativas[i].TryGetComponent(out React_Plataforma reactplataforma))
                {
                    reactplataforma.Ativar();
                }
                else
                {
                    if (plataformasDesativas[i].TryGetComponent(out Loop_Plataforma loopPlataforma))
                    {
                        loopPlataforma.Ativar();
                    }
                    else
                    {
                        if (plataformasDesativas[i].TryGetComponent(out Fade_Plataforma fadePlataforma))
                        {
                            fadePlataforma.Ativar();
                        }
                    }
                }
            }
        }
    }
}
