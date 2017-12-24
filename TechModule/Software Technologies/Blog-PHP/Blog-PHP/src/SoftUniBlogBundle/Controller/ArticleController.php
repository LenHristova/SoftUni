<?php

namespace SoftUniBlogBundle\Controller;

use SoftUniBlogBundle\Entity\Article;
use SoftUniBlogBundle\Form\ArticleType;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\Security;
use Symfony\Component\HttpFoundation\Request;


class ArticleController extends Controller
{
    /**
     * @param Request $request
     *
     * @Route("/article/create", name="article_create")
     * @Security("is_granted('IS_AUTHENTICATED_FULLY')")
     *
     * @return \Symfony\Component\HttpFoundation\RedirectResponse
     *
     */
    public function create(Request $request)
    {
        $article = new Article();
        $form = $this->createForm(ArticleType::class, $article);

        $form->handleRequest($request);

        $titleErrorMsg = "";
        $contentErrorMsg = "";
        $titleValue = $article->getTitle();
        $contentValue = $article->getContent();
        if ($form->isSubmitted() && $form->isValid()){

            if ($article->getTitle() === null || $article->getContent() === null){
                if ($article->getTitle() === null) {
                    $titleErrorMsg = "Please add title!";
                }
                if ($article->getContent() === null) {
                    $contentErrorMsg = "Please add content!";
                }
                return $this->render('article/create.html.twig',
                    array('titleErrorMsg' => $titleErrorMsg,
                        'contentErrorMsg' => $contentErrorMsg,
                        'titleValue' => $titleValue,
                        'contentValue' => $contentValue,
                        'form' => $form->createView()));
            }

            $article->setAuthor($this->getUser());
            $em = $this->getDoctrine()->getManager();
            $em->persist($article);
            $em->flush();

            return $this->redirectToRoute('blog_index');
        }

        return $this->render('article/create.html.twig',
            array('titleErrorMsg' => $titleErrorMsg,
                'contentErrorMsg' => $contentErrorMsg,
                'titleValue' => $titleValue,
                'contentValue' => $contentValue,
                'form' => $form->createView()));
    }

    /**
     * @Route("/article/{id}", name="article_view")
     * @param $id
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function viewArticle($id)
    {
        $article = $this->getDoctrine()->getRepository(Article::class)->find($id);

        return $this->render('article/article.html.twig', ['article' => $article]);
    }
}
